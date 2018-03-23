import React, { Component } from 'react';

class PhoneNumbersComponent extends Component {
    render() {
        return (
            this.props.numbers.map(n => {
                return (
                    <td key={n.number}>{n.number}</td>
                );
            })
        );
    }
}

export default PhoneNumbersComponent;

