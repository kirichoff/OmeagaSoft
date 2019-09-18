import React, {Component} from 'react';
import {Tabs, TabsItem} from 'rambler-ui/Tabs'
import {connect} from "react-redux";
import {bindActionCreators} from "redux";
import {actionCreators} from "./store/UsersReducer";
import {Link} from "react-router-dom";

const values = ['Login','Registration','Edit','Admin']



class Layout extends Component {

    render() {
        console.log(this.props)
        console.log( )
        return (
            <div>
                <Tabs value={this.props.routing.location.pathname || 'Login' } >
                    {values.map((item, index) => (
                       <TabsItem

                            value={item}
                            key={index}
                            className="customTabsItemClassName">
                           <Link to={`/${item === 'Login'? ''  : item}`}>
                            {item}
                       </Link>
                        </TabsItem>

                    ))}
                </Tabs>


                {this.props.children}
            </div>
        );
    }
}

export default connect(
    state => state,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Layout) ;