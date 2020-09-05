import React from 'react'
import { Listbox, Option, OptionsList } from 'listbox'

const WordList = (props) => {
    const { name, wordList } = props

    return (
        <div>
            <span><strong>{name}</strong></span>
            <Listbox
              style={{
                marginTop: "1em",
                maxHeight: "10em",
                overflowY: "auto",
                border: "rgba(128,128,128,0.25) 1px solid"
            }}>
                <OptionsList>
                {wordList.map((word, i) => (
                    <Option key={i}>
                        {word}
                    </Option>
                ))}
                </OptionsList>
            </Listbox>
        </div>
    )
}

export default WordList
