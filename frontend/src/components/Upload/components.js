import React from 'react'
import styled from 'styled-components'

import {
    UPLOAD_NOT_STARTED,
    UPLOAD_PENDING,
    UPLOAD_SUCCESS,
    UPLOAD_FAILED
} from './const'

const containerColor = (status) => {
    switch (status) {
        case UPLOAD_SUCCESS:
            return 'lightgreen'
        case UPLOAD_FAILED:
            return 'red'
        case UPLOAD_PENDING:
        default:
            return 'lightgray'
    }
}

const message = (status) => {
    switch (status) {
        case UPLOAD_SUCCESS:
            return 'SUCCESS'
        case UPLOAD_FAILED:
            return 'FAILED'
        case UPLOAD_PENDING:
        default:
            return 'PENDING'
    }
}

const Container = styled.section`
    display: ${({ status }) => status === UPLOAD_NOT_STARTED ? 'none' : 'flex'};
    background-color: ${({ status }) => containerColor(status)};

    & > h4 {
        flex-grow: 0.05;
    }

    & > div {
        flex-grow: 1;
    }
`

const ChildrenContainer = styled.div`
    display: ${({ status }) => status === UPLOAD_NOT_STARTED ? 'none' : 'flex'};

    & > p {
        flex-grow: 1;
    }

    & > button {
        flex-grow: 0.125;
    }
`

export const StatusMessage = (props) => {
    return (
        <Container status={props.status}>
            <h4>{message(props.status)}</h4>
            <ChildrenContainer status={props.status}>
                {props.children}
            </ChildrenContainer>
        </Container>
    )
}
