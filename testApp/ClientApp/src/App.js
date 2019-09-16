import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import TestCompponent from "./components/TestCompponent";
import LoginPage from "./components/LoginPage";
import {ApplyTheme} from  'rambler-ui/theme'
export default () => (

    <ApplyTheme>
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/counter' component={Counter} />
      <Route path='/test' component={TestCompponent} />
      <Route path='/Login' component={LoginPage} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
  </Layout>
    </ApplyTheme>
);
