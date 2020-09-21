import React, { useEffect, useState } from 'react'
import { Redirect } from 'react-router-dom'

import { postDictionaryList } from '../../services/api/dictionary'

import AppTitle from '../AppTitle'

import {
    UPLOAD_NOT_STARTED,
    UPLOAD_PENDING,
    UPLOAD_SUCCESS,
    UPLOAD_FAILED
} from './const'

import { StatusMessage } from './components'

const Upload = () => {
    const [wordsAdded, setWordsAdded] = useState(0)
    const [returnHome, setReturnHome] = useState(false)
    const [uploadState, setUploadState] = useState(UPLOAD_NOT_STARTED)

    if (returnHome) {
        return (
            <Redirect to="/" />
        )
    }

    return (
        <div>
            <AppTitle>Dictionary Assistant MVC</AppTitle>
            <StatusMessage status={uploadState}>
                <p>Words Added: {wordsAdded}</p>
                <button
                  type="button"
                  onClick={e => {
                      e.preventDefault()
                      setReturnHome(true)
                  }}>
                        OK
                </button>
            </StatusMessage>
            <section>
                <form
                  id="FileUploadForm"
                  encType="multipart/form-data"
                  onSubmit={e => {
                      e.preventDefault()
                      const fileInput = e.target.children['FilePicker']
                      const uploadThis = fileInput.files[0]

                      if (uploadThis !== undefined) {
                          setUploadState(UPLOAD_PENDING)

                          const contentData = new FormData();
                          contentData.append('wordListFile', uploadThis)

                          postDictionaryList(contentData)
                            .then(numAdded => {
                                setWordsAdded(numAdded)
                                setUploadState(UPLOAD_SUCCESS)
                            })
                            .catch(e => {
                                setUploadState(UPLOAD_FAILED)
                                console.log(e)
                            })
                      }
                      else {
                          setUploadState(UPLOAD_FAILED)
                      }
                  }}>
                        <label htmlFor="FilePicker">
                            Word List to Upload:
                        </label>
                        <input
                          type="file"
                          id="FilePicker"
                          accept=".txt" />
                        <button
                          type="button"
                          onClick={e => {
                              e.preventDefault()
                              setReturnHome(true)
                          }}>
                                Go Back
                        </button>
                        <button type="submit">
                            Add Words To Dictionary
                        </button>
                </form>
            </section>
        </div>
    )
}

export default Upload
