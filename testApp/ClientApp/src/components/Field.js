import React, {Component} from 'react';
import FormGroup from "rambler-ui/FormGroup";
import Input from "rambler-ui/Input";
import * as icons from "rambler-ui/icons/forms";
import Button from "rambler-ui/Button";
import InputStatus from 'rambler-ui/InputStatus'
import {EmailMatch, PasswordMatch} from "../utils/regexFunctions";
const Edit = icons['EditIcon'];
const Clear = icons['ClearIcon'];
const Check = icons['TickIcon']

class Field extends Component {
    constructor(props, context) {
        super(props, context);
        this.state={
            edit: false,
            value: '',
            error: false
        }
    }
    onChange = (e)=> this.setState({value: e.target.value})
    togle =()=> this.setState({edit: !this.state.edit})
    Confirm =()=>{

        switch (this.props.type){
            case 'email':
                if (EmailMatch( this.state.value))
                    {
                        this.setState({edit: false ,});
                this.props.onConfirm({[`${this.props.label}`] : this.state.value})
                    }
            else
                this.setState({error: true});
            break;
            case 'password': if (PasswordMatch(this.state.value))
                {this.setState({edit: false,});
                this.props.onConfirm({[`${this.props.label}`] : this.state.value})}
            else
                this.setState({error: true});
            break;
            default:  this.setState({edit: false ,});  this.props.onConfirm({[`${this.props.label}`] : this.state.value})
        }

    }


    render() {
        let type = this.props.type || 'text'
        return (
            <div>
                <FormGroup label={this.props.label}>

                    {
                        !this.state.edit?
                            <div className={'btn-container'} >

                                <div
                                    className={'text-width'}
                                >
                                    {this.props.value}
                                </div>
                                <Button
                                    icon={<Edit/>}
                                    onClick={this.togle}
                                iconPosition={'right'}>
                                    edit
                                </Button>
                            </div>
                            :
                            <div className={'input-field'}>
                                <InputStatus type="error" message={this.state.error? 'incorrect' : ''}  >
                            <Input
                                status={this.state.error? 'error' : null}
                                type={type}
                                style={{width: '100%'}}
                                value={this.state.value}
                                onChange={this.onChange}
                                placeholder={this.props.placeholder}
                                variation={"regular"}
                            />
                                </InputStatus>
                            <div  className={'btn-container'} >
                                <Button

                                    style={{marginTop: '2%'}}
                                    type={'primary'}
                                    onClick={this.Confirm}
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
                            </div>
                    }
                </FormGroup>
            </div>
        )
    }
}

export default Field;
