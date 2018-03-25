import React, { Component } from 'react';
import ContactComponent from './ContactComponent.js';

class TableComponent extends Component {
    render() {
        var contacts = this.props.contacts.slice();
        return (
        <table>
        <thead>
          <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>PhoneNumbers</th>
          </tr>
        </thead>

        <tbody>
          {this.props.contacts.map(c => {
            return (
              <tr key={c.id}>
                <ContactComponent contact={c} handleDelete={(contact)=>this.props.handleDeleteContact(contact)} />
              </tr>
            );
          })}
        </tbody>
      </table>
        );
    }
}

export default TableComponent; 