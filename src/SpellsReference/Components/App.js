﻿import React, { Component } from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Link
} from "react-router-dom";

import Index from './Spell/Index';
import Create from './Spell/Create';
import Filter from './Spell/Filter';
import Select from './Spell/Select';

import SpellbookIndex from './Spellbook/Index';

function App() {
    return (
        <Router>
                <Switch>
                    <Route path="/Spell/Select/:id" component={Select}>
                    </Route>
                    <Route path="/Spell/Filter">
                        <Filter />
                    </Route>
                    <Route path="/Spell/Create">
                        <Create />
                    </Route>
                    <Route path="/Spellbook">
                        <SpellbookIndex />
                    </Route>
                    <Route exact path="/">
                        <Index />
                    </Route>
                </Switch>
        </Router>
    );
}

export default App;