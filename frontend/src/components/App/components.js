import styled from 'styled-components'

export const AppTitle = styled.h1`
    border-bottom: gray 1px solid;
    padding-bottom: 0.40em;
    padding-left: 1vw;
`

export const SelectorSection = styled.section`
    border: rgba(128,128,128,0.25) 1px solid;
    padding: 0.75em;
    display: flex;

    & * {
        flex-grow: 0.25;
    }

    & select {
        margin: 1em;
    }
`

export const WordListHolder = styled.div`
    display: flex;

    & > * {
        flex-grow: 0.5;
        margin: 0 1em 0 1em;
    }
`
