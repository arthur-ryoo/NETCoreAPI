import React, { Component } from 'react';
import { Modal, Button, Row, Col, Form, Image } from 'react-bootstrap';

export default class AddEmpModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            deps: []
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleFileSelected = this.handleFileSelected.bind(this);
    }

    photofilename = "unnamed.png";
    imagesrc = process.env.REACT_APP_PHOTOPATH + this.photofilename;

    refreshList(){
        fetch(process.env.REACT_APP_API + 'department')
        .then(response => response.json())
        .then(data => {
            this.setState({
                deps: data
            });
        });
    }

    componentDidMount() {
        this.refreshList();
    }


    handleSubmit(event) {
        event.preventDefault();
        fetch(process.env.REACT_APP_API + 'employee', {
            method: 'POST',
            headers: {
                'Accept': 'Application/json',
                'Content-Type': 'Application/json'
            },
            body: JSON.stringify({
                EmployeeName: event.target.EmployeeName.value,
                Department: event.target.Department.value,
                DateOfJoining: event.target.DateOfJoining.value,
                PhotoFileName: this.photofilename
            })
        })
        .then(response => response.json())
        .then((result) => {
            alert(result);
        },
        (error) => {
            alert('Failed');
        })
    }

    handleFileSelected(event) {
        this.refreshList();
        event.preventDefault();
        this.photofilename = event.target.files[0].name;
        const formData = new FormData();
        formData.append(
            "myFile",
            event.target.files[0],
            event.target.files[0].name
        );

        fetch(process.env.REACT_APP_API + 'employee/savefile', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then((result) => {
            this.imagesrc = process.env.REACT_APP_PHOTOPATH + result;
        },
        (error) => {
            alert('Failed');
        })
        
    }

    render() {
        return (
            <div className="container">
                <Modal
                    {...this.props}
                    size="lg"
                    aria-labelledby="contained-modal-title-vcenter"
                    centered
                >
                    <Modal.Header closeButton>
                        <Modal.Title id="contained-modal-vcenter">
                            Add Employee
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="EmployeeName">
                                        <Form.Label>EmployeeName</Form.Label>
                                        <Form.Control type="text" name="EmployeeName" required 
                                        placeholder="Employee Name"/>
                                    </Form.Group>

                                    <Form.Group controlId="Department">
                                        <Form.Label>Department</Form.Label>
                                        <Form.Control as="select">
                                            {this.state.deps.map(dep => 
                                                <option key={dep.DepartmentId}>{dep.DepartmentName}</option>
                                            )}
                                        </Form.Control>
                                    </Form.Group>

                                    <Form.Group controlId="DateOfJoining">
                                        <Form.Label>DateOfJoining</Form.Label>
                                        <Form.Control 
                                        type="date"
                                        name="DateOfJoining"
                                        required
                                        placeholder="DateOfJoining"
                                        />
                                    </Form.Group>

                                    <Form.Group>
                                        <Button variant="primary" type="submit">
                                            Add Employee
                                        </Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                            <Col sm={6}>
                                <Image width="200px" height="200px" src={this.imagesrc}/>
                                <input onChange={this.handleFileSelected} type="File"/>
                            </Col>
                        </Row>
                    </Modal.Body>

                    <Modal.Footer>
                        <Button variant="danger" onClick={this.props.onHide}>Close</Button>
                    </Modal.Footer>
                </Modal>
            </div>      
            
        )
    }

}
