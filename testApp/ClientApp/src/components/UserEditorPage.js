import React, {Component} from 'react';
import Field from "./Field";
import {connect} from "react-redux";
import {bindActionCreators} from "redux";
import {actionCreators} from "../store/UsersReducer";
import '../style/Editor.css'
import {RadioButton, RadioButtonGroup} from "rambler-ui/Radio";
import {Link} from "react-router-dom";
class UserEditorPage extends Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            Type: 'User',
            error: false
        }
    }
    onChange = (e)=> {
        console.log('E',e.target.value)
        this.props.Edit({objectValue: e.target.value})
    }

     confirm  = e  => {
        this.props.Edit({...e}).then(
            res=> res? console.log('res') : null
        )
    }

    render() {
        console.log(this.props)
        let User = this.props.User.User || {}
        console.log(User)
    return(
        <div className={'editor-container'} >
            {
                User.UserName?
                    <div style={{width: '100%'}}>
                        <Field isError={this.state.error}  onConfirm={this.confirm}  placeholder={User.FirstName}  value={User.FirstName} type={'text'} label={'FirstName'}/>
            <Field isError={this.state.error} onConfirm={this.confirm} placeholder={User.LastName} value={User.LastName} type={'text'} label={'LastName'}/>
            <Field isError={this.state.error} onConfirm={this.confirm} placeholder={'Password'} value={'Password'} type={'password'} label={'Password'}/>
            <Field isError={this.state.error}  onConfirm={this.confirm} placeholder={User.Email} value={User.Email} type={'email'} label={'Email'}/>

            <RadioButtonGroup
                style={{marginBottom: 40, maxWidth: 300}}
                value={User.Type? 'Admin' : 'User'}
                onChange={this.onChange}>

                <RadioButton  value={'Admin'}>
                    Admin
                </RadioButton>
                <RadioButton  value={'User'}>
                    User
                </RadioButton>
            </RadioButtonGroup>
                    </div>:
                <div style={{textAlign:'center'}} >
                Please <Link to ={'/'}>Login</Link>  in or <Link to ={'/Registration'}>Register</Link>
                </div>
            }
        </div>
        )
    }
}

export default
connect(
    state => state,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(UserEditorPage);
