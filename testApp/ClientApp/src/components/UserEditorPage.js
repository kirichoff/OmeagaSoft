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
            Type: 'User'
        }
    }


    onChange = (e)=> {
        this.props.Edit({objectValue: this.state.Type})
            this.setState({Type: e.target.value})
    }

    confirm = e =>{
        this.props.Edit({...e})
    }

    render() {
        console.log(this.props)
        let User = this.props.User.User || {}
        console.log(User)
    return(
        <div className={'editor-container'} >
            {
                User.UserName?
                    <div>
                <Field onConfirm={this.confirm} value={User.FirstName} type={'text'} label={'FirstName'}/>
            <Field onConfirm={this.confirm} value={User.LastName} type={'text'} label={'LastName'}/>
            <Field  onConfirm={this.confirm} value={User.Password} type={'password'} label={'Password'}/>
            <Field  onConfirm={this.confirm} value={User.Email} type={'email'} label={'Email'}/>

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

                       <Link to={'Admin'}>Admin Page</Link>

                    </div>:
                <div>
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
