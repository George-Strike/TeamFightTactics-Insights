import React, { Component } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import { NavMenu } from './NavMenu';
import './layout.css';

export class Layout extends Component {
    displayName = Layout.name

    render() {
        return (
            <div>
                <Container fluid>
                    <Row>
                        <Col sm={12}>
                            <NavMenu />
                        </Col>
                    </Row>
                </Container>
                <Container fluid id="core-container">
                    <Row>
                        <Col md={12}>
                            {this.props.children}
                        </Col>
                    </Row>
                </Container>
            </div>
        );
    }
}
