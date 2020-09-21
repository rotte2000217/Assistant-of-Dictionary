import React from 'react';
import ReactDOM from 'react-dom';
import { createGlobalStyle } from 'styled-components'
import App from './components/App';

const GlobalStyle = createGlobalStyle`
    body {
      margin: 0;
      font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen',
        'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue',
        sans-serif;
      -webkit-font-smoothing: antialiased;
      -moz-osx-font-smoothing: grayscale;
    }

    code {
      font-family: source-code-pro, Menlo, Monaco, Consolas, 'Courier New',
        monospace;
    }

    section {
        margin: 3em;
    }

    #AddMoreLink {
        align-self: center;
        text-align: right;
    }

    #FileUploadForm {
        display: flex;
        flex-direction: column;
        border: solid 1px gray;
        padding: 1rem;
    }
`

ReactDOM.render(
  <React.StrictMode>
    <GlobalStyle />
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);
