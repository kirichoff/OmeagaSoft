import React, {Component} from 'react';
import {Tabs, TabsItem} from 'rambler-ui/Tabs'
import {connect} from "react-redux";
import {bindActionCreators} from "redux";
import {actionCreators} from "../store/UsersReducer";
import {Link} from "react-router-dom";
import '../style/Layout.css'
const values = ['Login','Registration','Editor','Admin']



class Layout extends Component {

    render() {
        console.log(this.props)
        console.log( )
        // let path = this.props.routing.location.pathname || ''
        // path = path.slice(1);
        let path='';
        console.log('Path',path)
        return (
            <div>

                <div className={'Layout'} >
                <Tabs value={path===''? 'Login' : path}  >
                    {values.map((item, index) => (
                       <TabsItem
                            container={<Link to={`/${item === 'Login'? ''  : item}`}>         </Link>}
                            value={item}
                            key={index}
                            className="customTabsItemClassName">
                            {item}

                        </TabsItem>

                    ))}
                </Tabs>
                </div>


                {this.props.children}
            </div>
        );
    }
}

export default connect(
    state => state,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Layout) ;