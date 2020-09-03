import React from 'react'
import { Chart, Interval, Tooltip } from 'bizcharts'
import { ChartDisplayer } from './components'

const ChartDisplay = (props) => {
    return (
        <ChartDisplayer current={props.currentChart} actual={props.chartID}>
            <Chart height={400} autoFit data={props.chartData} force>
                <Interval position={props.interval} />
                <Tooltip shared />
            </Chart>
        </ChartDisplayer>
    )
}

export default ChartDisplay
