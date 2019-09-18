import React, {Component} from 'react';
import FormGroup from "rambler-ui/FormGroup";
import Input from "rambler-ui/Input";
import Button from "rambler-ui/Button";
import {RadioButton, RadioButtonGroup} from 'rambler-ui/Radio'
import '../style/Registratinon.css'
import {connect} from "react-redux";
import {bindActionCreators} from "redux";
import {actionCreators} from "../store/UsersReducer";
import {Link} from "react-router-dom";
import EmailMatch from "../EmailMatch";
import InputStatus from "rambler-ui/InputStatus";


class RegistrationPage extends Component {

    constructor(props, context) {
        super(props, context);
        this.state = {
            UserName: null,
            Password: null,
            objectValue: 'Admin',
            FirstName: null,
            LastName: null,
            Email: null,
            status: ''
        }
    }

    onChange = (tag)=> (e)=>this.setState({[`${tag}`]: e.target.value});
    click = ()=> {
        if (EmailMatch(this.state.Email)) {
            this.props.Register({...this.state})
            this.setState({status: ''})
        }
        else {this.setState({status: 'error'})}
    }

    onObjectsChange = (e)=> this.setState({objectValue:e.target.value})


    render() {
        console.log(this.props)
        console.log(this.state)
        if (this.props.User.isRegister === true){
            this.props.history.push('/')
        }
        return (
                <div className={'registration-container'}>
                    <FormGroup label='User Name'>
                        <Input
                            type="text"
                            value={this.state.UserName || ''}
                            onChange={this.onChange('UserName')}
                            placeholder="User Name"
                            variation={"regular"}
                        />
                    </FormGroup>
                    <FormGroup label='Firs Name'>
                        <Input
                            type="text"
                            value={this.state.FirstName || ''}
                            onChange={this.onChange('FirstName')}
                            placeholder="User Name"
                            variation={"regular"}
                        />
                    </FormGroup>
                    <FormGroup label='Last Name'>
                        <Input
                            type="text"
                            value={this.state.LastName ||''}
                            onChange={this.onChange('LastName')}
                            placeholder="User Name"
                            variation={"regular"}
                        />
                    </FormGroup>

                    <FormGroup label='Email'>
                        <InputStatus type={this.state.status||null} message={this.state.status==="error"?'incorrect email':''}  >
                        <Input
                            status={this.state.status|| null}
                            type="email"
                            value={this.state.Email||''}
                            onChange={this.onChange('Email')}
                            placeholder="User Name"
                            variation={"regular"}
                        />
                        </InputStatus>
                    </FormGroup>
                    <FormGroup label='password'>
                        <Input
                            type="password"
                            value={this.state.Password||''}
                            onChange={this.onChange('Password')}
                            placeholder="Password"
                            variation={"regular"}
                        />
                    </FormGroup>

                    <RadioButtonGroup
                        style={{marginBottom: 40, maxWidth: 300}}
                        value={this.state.objectValue}
                        onChange={this.onObjectsChange}>

                            <RadioButton  value={'Admin'}>
                                Admin
                            </RadioButton>
                        <RadioButton  value={'User'}>
                            User
                        </RadioButton>
                    </RadioButtonGroup>

                    <Button
                        style={{marginTop: '2%'}}
                        type={'primary'}
                        onClick={this.click}
                    >
                        Confirm Button
                    </Button>
                </div>
        );
    }
}

export default connect(
    state => state,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(RegistrationPage);
