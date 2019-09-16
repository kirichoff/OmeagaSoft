import React, {Component} from 'react';
import FormGroup from "rambler-ui/FormGroup";
import Input from "rambler-ui/Input";
import * as icons from "rambler-ui/icons/forms";
import Button from "rambler-ui/Button";

const Edit = icons['EditIcon'];
const Clear = icons['ClearIcon'];
const Check = icons['TickIcon']

class Field extends Component {
    constructor(props, context) {
        super(props, context);
        this.state={
            edit: false,
            value: ''
        }
    }

    onChange = (e)=> this.setState({value: e.target.value})
    togle =()=> this.setState({edit: !this.state.edit})
    render() {
        return (
            <div>
                <FormGroup label={this.props.label}>

                    {
                        !this.state.edit?
                            <div>
                                {this.props.value}
                                <Edit onClick={this.togle}  />
                            </div>
                            :
                            <div>
                            <Input
                                type="text"
                                style={{width: '40%'}}
                                value={this.state.value}
                                onChange={this.onChange}
                                placeholder="User Name"
                                variation={"regular"}
                            />
                                <Button
                                    style={{marginTop: '2%'}}
                                    type={'primary'}
                                    onClick={this.togle}
                                >
                                    Confirm
                                </Button>
                                <Button
                                    style={{marginTop: '2%'}}
                                    type={'primary'}
                                    onClick={this.togle}
                                >
                                    cancel
                                </Button>
                            </div>
                    }
                </FormGroup>
            </div>
        )
    }
}

export default Field;