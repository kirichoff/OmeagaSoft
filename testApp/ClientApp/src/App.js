import React from 'react';
import { Route } from 'react-router';
import TestCompponent from "./components/TestCompponent";
import LoginPage from "./components/LoginPage";
import {ApplyTheme} from  'rambler-ui/theme'
import RegistrationPage from "./components/RegistrationPage";
import UserEditorPage from "./components/UserEditorPage";
import AdministrationPage from "./components/AdministrationPage";
import {Router} from "react-router-dom";
export default (props) => (
<Router history={props.history}>
    <ApplyTheme>
      <div>

      <Route path='/test' component={TestCompponent} />
      <Route path='/Login' component={LoginPage} />
      <Route path='/Registration' component={RegistrationPage} />
      <Route path='/Editor' component={UserEditorPage} />
    <Route path='/Admin' component={AdministrationPage} />

      </div>
    </ApplyTheme>
</Router>
);
