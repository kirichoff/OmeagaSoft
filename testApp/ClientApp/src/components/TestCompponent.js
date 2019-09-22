import React, {Component} from 'react';
import rest from '../rest'
import dotenv  from 'dotenv'
class TestCompponent extends Component {

    componentWillMount() {
        // const user = {
        //   UserName: "qwe",
        //   FirstName:  "sss",
        //   LastName: "ss",
        //   Password: "bbb",
        //   Email: "trr",
        //   Type: true,
        //   StartDate: new Date()
        // }
        // rest.Modify(user,'add')
    }

    cl =()=>{
            rest.getUsers().then(d=>console.log(d))
        }

        cl2=()=>{
        console.log('cl')
            //https://randomuser.me/api/?results=10
        let url = 'https://localhost:44336/api/values/GetAllUsers'
            fetch(url,{method: 'GET',})
                .then(res=>console.log('res',res.text().then(r=>console.log(r))))
                .catch(er=>console.log('err',er))
                .finally(k=>console.log(k,'finally'))
        }

    render() {
        console.log(process.env)

        return (
            <div>
            <button onClick={this.cl}>btn</button>
                <button onClick={this.cl2}>btn2</button>
            </div>
        );
    }
}

export default TestCompponent;
