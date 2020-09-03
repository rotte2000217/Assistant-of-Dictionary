import React, { useEffect, useState } from 'react'
import { getAllWordCounts, getTopWordCounts } from '../../services/api/dictionary'
import ChartDisplay from '../ChartDisplay'

import { AppTitle, SelectorSection } from './components'

const TOP_FIVE = 1
const ALL_OF_THEM = 2

const App = () => {
    const [graphThis, setGraphThis] = useState(TOP_FIVE)
    const [topFiveData, setTopFiveData] = useState([])
    const [allData, setAllData] = useState([])

    // Run on component mount
    useEffect(() => {
        getTopWordCounts(5)
            .then(data => setTopFiveData(data))
            .then(() => getAllWordCounts())
            .then(data => setAllData(data))
    }, [])

    if (allData.length === 0 || topFiveData.length === 0) {
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
