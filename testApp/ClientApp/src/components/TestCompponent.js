import React, {Component} from 'react';
import rest from '../rest'

class TestCompponent extends Component {

    componentWillMount() {
        const user = {
          UserName: "qwe",
          FirstName:  "sss",
          LastName: "ss",
          Password: "bbb",
          Email: "trr",
          Type: true,
          StartDate: new Date()
        }
        rest.Modify(user,'add')
    }

    cl =()=>{
            rest.getUsers().then(d=>console.log(d))
        }

    render() {
        return (
            <div>
            <button onClick={this.cl}>btn</button>
            </div>
        );
    }
}

export default TestCompponent;