import React, {Component} from 'react';
import {Table} from "react-bootstrap";
import {connect} from "react-redux";
import {bindActionCreators} from "redux";
import {actionCreators} from "../store/UsersReducer";
import {sort} from '../utils/sort'
import Input from "rambler-ui/Input";
import Select from 'rambler-ui/Select'
import {MenuItem} from 'rambler-ui/Menu'
import '../style/Admin.css'
import {Link} from "react-router-dom";

class AdministrationPage extends Component {

    constructor(props, context) {
        super(props, context);
        this.state={
            value: '',
            selector: '',
            ByDate: false,
        }
    }


    componentWillMount() {
        this.props.getJournal()
    }
    dateChange = ()=> this.setState({ByDate: !this.state.ByDate});
    change = (e)=> this.setState({value: e.target.value})
    changeS =(e)=> this.setState({selector:  e })

    render() {


        let User = this.props.User.User || {}

        let journal = this.props.User.Journal !== 'false' && this.props.User.Journal?  this.props.User.Journal: []

        console.log(journal)

        let sorted = journal;

        if (   this.state.value !== '' && this.state.selector === ''){
            sorted = sort.byName(journal,this.state.value)
            if (this.state.ByDate) sorted = sort.ByDate(sorted)
        }
        if( (this.state.selector !== '') && ( this.state.value === '')){
            sorted = sort.byAction(sorted,this.state.selector)
            if (this.state.ByDate) sorted = sort.ByDate(sorted)
        }
        if( (this.state.selector) !== '' && (this.state.value !== '')){
            sorted = sort.byName(journal,this.state.value)
            sorted = sort.byAction(sorted,this.state.selector)
            if (this.state.ByDate) sorted = sort.ByDate(sorted)
        }
        if (this.state.ByDate) sorted = sort.ByDate(sorted)

        console.log(User)

        return (
            <div>


              {
                  User.Type?
                  <Table >
                    <thead>
                    <tr>
                    <th>
                        <div className={'table-header'}>
                        <div  className={'center'}> User = </div >
                            <div    style={{width: '30%'}} >
                                <Input size={'small'} value={this.state.value} onChange={this.change}  />
                            </div>
                        </div>
                    </th>
                    <th
                        onClick={this.dateChange}
                        style={{color: this.state.ByDate? 'green' : 'red' }}
                    >
                        Date
                    </th>
                    <th>

                        <div className={'table-header'}>
                        <div className={'center'}>Action = </div>
                        <div
                            style={{width: '40%'}}
                        >
                        <Select
                            size={'small'}
                            placeholder="Select..."
                            status="success"
                            value={this.state.selector}
                            onChange={this.changeS}>
                                <MenuItem value={'Update'} >
                                    Update
                                </MenuItem>
                            <MenuItem value={'Register'} >
                                Register
                            </MenuItem>
                            <MenuItem value={'Login'} >
                                Login
                            </MenuItem>
                            <MenuItem value={''} >
                                select...
                            </MenuItem>
                        </Select></div>
                        </div>
                    </th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        sorted.map(
                        k=>
                            <tr key={k.Id}>
                                <th>{k.UserName || ''}</th>
                                <th>{ new Date(k.Date).toDateString()} | {new Date(k.Date).getHours()}:{new Date(k.Date).getMinutes()}</th>
                                <th>{k.Action}</th>
                            </tr>
                    )
                    }


                    </tbody>
                </Table>
              :
                  <div style={{margin: 'auto',textAlign: 'center',marginTop: '20%'}} >
                  Please <Link to ={'/'}>Login</Link>  in or <Link to ={'/Registration'}>Register</Link>
                  </div>
              }
            </div>
        );
    }
}

export default connect(
    state => state,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(AdministrationPage);
