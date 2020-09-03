import styled from 'styled-components'

export const ChartDisplayer = styled.section`
    display: ${(props) => props.current === props.actual ? 'inherit' : 'none'};
`
