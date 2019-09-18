import React, {Component} from 'react';
import FormGroup from "rambler-ui/FormGroup";
import Input from "rambler-ui/Input";
import * as icons from "rambler-ui/icons/forms";
import Button from "rambler-ui/Button";
import InputStatus from 'rambler-ui/InputStatus'
import EmailMatch from "../EmailMatch";
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
    Confirm =()=>{
        if(this.props.type==='email' && EmailMatch( this.state.value) ){

        }
        if (this.props.type !=='email')
            this.props.onConfirm({[`${this.props.label}`] : this.state.value})

        this.setState({edit: false,})
    }


    render() {

        let type = this.props.type || 'text'
        console.log(this.props.type)
        console.log(type)
        return (
            <div>
                <FormGroup label={this.props.label}>

                    {
                        !this.state.edit?
                            <div className={'btn-container'} >

                                <div
                                    className={'text-width'}
                                >{this.props.value}
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
                                <InputStatus type="error" message={'error'}  >
                            <Input
                                status={'error'}
                                type={type}
                                style={{width: '40%'}}
                                value={this.state.value}
                                onChange={this.onChange}
                                placeholder="User Name"
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
