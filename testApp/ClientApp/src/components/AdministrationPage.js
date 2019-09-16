import React, {Component} from 'react';
import {Table} from "react-bootstrap";

class AdministrationPage extends Component {
    render() {
        return (
            <div>
                <Table className={'table'}>
                    <thead>
                    <tr>
                    <th>User</th>
                    <th>Date</th>
                    <th>Action</th>
                    </tr>
                    </thead>
                    <tbody>

                    </tbody>
                </Table>
            </div>
        );
    }
}

export default AdministrationPage;