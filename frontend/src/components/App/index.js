import React, { useEffect, useState } from 'react'
import {
    getAllWordCounts, getTopWordCounts, getAllLetterStatistics, getTopFiftyLongest,
    getTopFiftyShortest
} from '../../services/api/dictionary'

import ChartDisplay from '../ChartDisplay'
import LetterStatistics from '../LetterStatistics'
import WordList from '../WordList'
import { AppTitle, SelectorSection, WordListHolder } from './components'

// Values for the alphabet
const ALPHABET_ARRAY = 'abcdefghijklmnopqrstuvwxyz'.split('')

// Values for the Graph Selector
const TOP_FIVE = 1
const ALL_OF_THEM = 2

const App = () => {
    const [currentLetter, setCurrentLetter] = useState(ALPHABET_ARRAY[0])
    const [graphThis, setGraphThis] = useState(TOP_FIVE)
    // This data is for letter statistics
    const [letterStats, setLetterStats] = useState({})
    // This data is for longest & shortest Words
    const [longestWords, setLongestWords] = useState({})
    const [shortestWords, setShortestWords] = useState({})
    // This data is for the charts
    const [topFiveData, setTopFiveData] = useState([])
    const [allData, setAllData] = useState([])

    // Run on component mount
    useEffect(() => {
        getTopWordCounts(5)
            .then(data => setTopFiveData(data))
            .then(() => getAllWordCounts())
            .then(data => setAllData(data))
            .then(() => getAllLetterStatistics())
            .then(data => setLetterStats(data))
            .then(() => getTopFiftyLongest())
            .then(data => setLongestWords(data))
            .then(() => getTopFiftyShortest())
            .then(data => setShortestWords(data))
    }, [])

    if (allData.length === 0 ||
        topFiveData.length === 0 ||
        Object.keys(letterStats).length === 0 ||
        Object.keys(shortestWords).length === 0 ||
        Object.keys(longestWords).length === 0) {
            return (
                <div>
                    <p>LOADING...</p>
                </div>
            )
    }

    return (
        <div>
            <AppTitle>Dictionary Assistant MVC</AppTitle>
            <SelectorSection>
                <p>Select a letter of the alphabet to view statistics about:</p>
                <select
                  name="specificLetter"
                  value={currentLetter}
                  onChange={(event) => setCurrentLetter(event.target.value)}>
                        {ALPHABET_ARRAY.map(abc => (
                            <option key={abc} value={abc}>
                                {abc.toUpperCase()}
                            </option>
                        ))}
                </select>
            </SelectorSection>
            <section>
                <LetterStatistics letterData={letterStats[currentLetter]} />
                <hr />
                <WordListHolder>
                    <WordList name="Top 50 Longest Words" wordList={longestWords[currentLetter]} />
                    <WordList name="Top 50 Shortest Words" wordList={shortestWords[currentLetter]} />
                </WordListHolder>
            </section>
            <SelectorSection>
                <p>Select a statistic of the loaded dictionary that you wish to display:</p>
                <select
                  name="whatToGraph"
                  value={graphThis}
                  onChange={(event) => setGraphThis(Number(event.target.value))}>
                        <option value={TOP_FIVE}>
                            Top 5 Words in Dictionary
                        </option>
                        <option value={ALL_OF_THEM}>
                            All Words in Dictionary
                        </option>
                </select>
            </SelectorSection>
            <ChartDisplay
              chartID={TOP_FIVE}
              currentChart={graphThis}
              chartData={topFiveData}
              interval="letter*wordCount" />
            <ChartDisplay
              chartID={ALL_OF_THEM}
              currentChart={graphThis}
              chartData={allData}
              interval="letter*wordCount" />
        </div>
    )
}

export default App
