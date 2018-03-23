import React, { Component } from 'react';
import PhoneNumbersComponent from './PhoneNumbersComponent.js'

class ContactComponent extends Component {
    render() {
        var cells = [];
        cells.push(<td key={1}>{this.props.contact.firstName}</td>);
        cells.push(<td key={2}>{this.props.contact.lastName}</td>);
        if (this.props.contact.phoneNumbers !== undefined && this.props.contact.phoneNumbers.length > 0) {
            cells.push(<PhoneNumbersComponent key={3} numbers={this.props.contact.phoneNumbers} />);
        }
        cells.push(<td key={4}><button onClick={() => this.props.handleDelete(this.props.contact)}>Delete contact</button></td>)

        return cells;
    }
}

export default ContactComponent; 