import React, {Component} from 'react';
import Field from "./Field";
import {connect} from "react-redux";
import {bindActionCreators} from "redux";
import {actionCreators} from "../store/UsersReducer";
class UserEditorPage extends Component {

    render() {
        console.log(this.props)
    return(
        <div>
            <Field value={'Name'} label={'First Name'}/>
            <Field value={'Name'} label={'Last Name'}/>
            <Field value={'Name'} label={'Password'}/>
            <Field value={'Name'} label={'Email'}/>
        </div>
        )
    }
}

export default
connect(
    state => state,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(UserEditorPage);