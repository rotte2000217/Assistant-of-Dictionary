import 'whatwg-fetch'

/**
 * Parses the JSON returned by a network request
 *
 * @param  {object} response A response from a network request
 *
 * @return {object}          The parsed JSON from the request
 */
const parseJSON = (response) => {
  if (response.status === 204 || response.status === 205) {
    return null
  }
  return response.json()
}

/**
 * Checks if a network request came back fine, and throws an error if not
 *
 * @param  {object} response   A response from a network request
 *
 * @return {object|undefined} Returns either the response, or throws an error
 */
const checkStatus = (response) => {
  if (response.status >= 200 && response.status < 300) {
    return response
  }
  const error = new Error(response.statusText)
  error.response = response
  throw error
}

/**
 * Perform a CRUD operation at the specified URL, returning a promise
 *
 * @param {string}      url       The URL to perform the operation on
 * @param {string}      operation The 'operation' to perform: GET, POST, etc...
 * @param {object|null} data      OPTIONAL: The data to send as the request body
 * @param {object|null} options   OPTIONAL: Any extra options for "fetch"
 *
 * @return {object}               A Fetch API Response Object
 */
export const crudOperation = (url, operation, data, options) => {
    const theData = data === null || data === undefined
        ? {}
        : {
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }
    const anyOptions = options === null || options === undefined
        ? {}
        : options

    return fetch(url, {
          method: operation,
          ...theData,
          ...anyOptions
      })
      .then(checkStatus)
}

/**
 * Requests a URL, returning a promise
 *
 * @param  {string} url       The URL we want to request
 * @param  {object} [options] The options we want to pass to "fetch"
 *
 * @return {object}           The response data
 */
const request = (url, options) => {
  // eslint-disable-next-line no-undef
  return fetch(url, options)
    .then(checkStatus)
    .then(parseJSON)
}

export default request
