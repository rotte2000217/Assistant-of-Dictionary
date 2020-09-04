import React from 'react'

import { Data } from './components'

const LetterStatistics = (props) => {
    const { numBegin, numEnd, avgCount } = props.letterData

    return (
        <div>
            <Data statistic="begin">{numBegin}</Data>
            <Data statistic="end">{numEnd}</Data>
            <Data statistic="average">{avgCount}</Data>
        </div>
    )
}

export default LetterStatistics
