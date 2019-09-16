import React, {Component} from 'react';
import FormGroup from "rambler-ui/FormGroup";
import Input from "rambler-ui/Input";
import Button from "rambler-ui/Button";

class UserEditorPage extends Component {
    render() {
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
                    <FormGroup label='Firs Name'>
                        <Input
                            type="text"
                            value={this.state.UserName}
                            onChange={this.onChange('UserName')}
                            placeholder="User Name"
                            variation={"regular"}
                        />
                    </FormGroup>
                    <FormGroup label='Last Name'>
                        <Input
                            type="text"
                            value={this.state.UserName}
                            onChange={this.onChange('UserName')}
                            placeholder="User Name"
                            variation={"regular"}
                        />
                    </FormGroup>
                    <FormGroup label='Email'>
                        <Input
                            type="email"
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

export default UserEditorPage;