import React, {Component} from 'react';
import Input from 'rambler-ui/Input'
import Button from 'rambler-ui/Button'
import '../style/Login.css'
import FormGroup from 'rambler-ui/FormGroup'
class LoginPage extends Component {
    constructor(props, context) {
        super(props, context);
        this.state = {
            UserName: "",
            Password: ""
        }
    }

    onChange = (tag)=> (e)=>this.setState({[`${tag}`]: e.target.value});
    click = ()=> {

    }

    render() {
        console.log(this.state)
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

export default LoginPage;