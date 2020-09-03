import request from './request'
import BASE_URL from './server'

export const getAllWordCounts = () =>
    request(`${BASE_URL}/api/dictionary/all-letters`)
      .then(array => array.map(j => ({ letter: j.letter, wordCount: j.numberWordsBeginningWith })))

export const getTopWordCounts = (cuantos) =>
    request(`${BASE_URL}/api/dictionary/top-letters/${cuantos}`)
      .then(array => array.map(j => ({ letter: j.letter, wordCount: j.numberWordsBeginningWith })))
