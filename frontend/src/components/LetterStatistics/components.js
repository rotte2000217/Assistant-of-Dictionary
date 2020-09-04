import styled from 'styled-components'

const beforeContent = (selector) => {
    switch(selector) {
        case 'begin':
            return 'Number of Words Beginning with Letter:'
        case 'end':
            return 'Number of Words Ending with Letter:'
        case 'average':
            return 'Average Number of Characters in Words with Letter:'
        default:
            return 'UNKNOWN:'
    }
}

export const Data = styled.p`
    &::before {
        content: '${({ statistic }) => beforeContent(statistic)}';
        font-weight: bold;
        padding-right: 1rem;
    }
`
