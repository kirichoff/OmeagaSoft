import React from 'react';
import { Route } from 'react-router';
import TestCompponent from "./components/TestCompponent";
import LoginPage from "./components/LoginPage";
import {ApplyTheme} from  'rambler-ui/theme'
import RegistrationPage from "./components/RegistrationPage";
import UserEditorPage from "./components/UserEditorPage";
import AdministrationPage from "./components/AdministrationPage";
import {Router} from "react-router-dom";
import Layout from "./components/Layout";
export default (props) => (

    <ApplyTheme>
      <Layout history={props.history}>
      <Route exact path='/test' component={TestCompponent} />
      <Route  exact path='/' component={LoginPage} />
          <Route  exact path='/Login' component={LoginPage} />
      <Route exact path='/Registration' component={RegistrationPage} />
      <Route exact path='/Editor' component={UserEditorPage} />
    <Route  exact path='/Admin' component={AdministrationPage} />
      </Layout>
    </ApplyTheme>

);
