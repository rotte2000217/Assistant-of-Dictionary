import React from 'react'
import { BrowserRouter, Switch, Route } from 'react-router-dom'

import Home from '../Home'
import Upload from '../Upload'

const App = () => {
    return (
        <BrowserRouter>
            <Switch>
                <Route path="/addwords">
                    <Upload />
                </Route>
                <Route path="/">
                    <Home />
                </Route>
            </Switch>
        </BrowserRouter>
    )
}

export default App
