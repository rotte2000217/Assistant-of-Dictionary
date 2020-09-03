import styled from 'styled-components'

export const AppTitle = styled.h1`
    border-bottom: gray 1px solid;
    padding-bottom: 0.40em;
`

export const SelectorSection = styled.section`
    display: flex;

    & * {
        flex-grow: 0.25;
    }

    & select {
        margin: 1em;
    }
`
