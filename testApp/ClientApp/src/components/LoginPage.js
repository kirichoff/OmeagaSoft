import React, {Component} from 'react';
import Input from 'rambler-ui/Input'
import Button from 'rambler-ui/Button'
import '../style/Login.css'
import FormGroup from 'rambler-ui/FormGroup'
import {Link} from "react-router-dom";
import {connect} from "react-redux";
import {bindActionCreators} from "redux";
import {actionCreators} from "../store/UsersReducer";
import {StoreHistory} from '../storeHistory'


class LoginPage extends Component {
    constructor(props, context) {
        super(props, context);
        this.state = {
            UserName: "",
            Password: ""
        }
    }

    onChange = (tag)=> (e)=>this.setState({[`${tag}`]: e.target.value});
    click = () => {
        this.props.Login(this.state.UserName,this.state.Password)

    }

    render() {
        console.log(this.props);

        if(this.props.User.User) true.props.history.push('/Editor')
        return (
            <div className={'login-container'}>
            <FormGroup label='User Name'>
                <Input
                    type="text"
                    value={this.state.UserName}
                    onChange={this.onChange('UserName')}
                    placeholder="User Name"
                    variation={"regular"}
                />
                </FormGroup>
                <FormGroup label='password'>
                <Input
                    type="password"
                    value={this.state.Password}
                    onChange={this.onChange('Password')}
                    placeholder="Password"
                    variation={"regular"}
                />
                </FormGroup>
                <div className={'btn-container'} >

                <Button
                    style={{marginTop: '2%'}}
                    type={'primary'}
                    onClick={this.click}
                >
                    Confirm Button
                </Button>

            <Link

                to={'/Registration'}>
                <Button
                    style={{marginTop: '2%'}}
                    type={'primary'}
                    onClick={this.click}
                >
                  Register
                </Button>
            </Link>
                </div>
            </div>
        );
    }
}

export default connect(
    state => state,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(LoginPage);
