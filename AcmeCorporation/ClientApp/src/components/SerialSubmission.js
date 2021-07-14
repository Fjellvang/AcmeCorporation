import React, { Component } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import authService from './api-authorization/AuthorizeService'

export class Counter extends Component {
	static displayName = Counter.name;

	constructor(props) {
		super(props);

		this.state = {
			isAuthenticated: false,
			userName: null
		};
	}


	componentDidMount() {
		this._subscription = authService.subscribe(() => this.populateState());
		this.populateState();
	}

	async populateState() {
		const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
		this.setState({
			isAuthenticated,
			userName: user && user.name
		});
	}

	setModal = (bool, text) => {
		console.log(bool);
		const state = this.state;
		state.modalText = text;
		state.modal = bool;
		this.setState(state);
	}


	submitForm = async (e) => {
		e.preventDefault();
		const target = e.target;

		const aboveEighteen = target.aboveEighteen.checked;
		if (!aboveEighteen) {
			this.setModal(true, "Only people above 18 are allowed to enter the draw");
			return;
		}

		const password = target.password.value;
		const email = target.email.value;
		const firstName = target.firstName.value;
		const lastName = target.lastName.value;
		const serial = target.serial.value;

		const response = await fetch('api/draw/submitDraw', {
			method: 'POST',
			headers: { 'Content-Type': ' application/json'},
			body: JSON.stringify({email, password, firstName, lastName, serial, aboveEighteen}),
		});
		if (response.ok) {
			authService.signIn({ returnUrl: '/draw' });
		}
		var responseText = await response.json();
		//TODO: Add proper errors here...
		this.setModal(true, responseText.title);
	}
	submitFormAuthorized = async (e) => {
		e.preventDefault();
		const target = e.target;
		const serial = target.serial.value;
		const token = await authService.getAccessToken();
		const response = await fetch(`api/draw/SubmitDrawAuthorized?serial=${serial}`, {
			method: 'POST',
			headers: { 'Authorization': `Bearer ${token}`, 'Content-Type': ' application/json' },
			//body: JSON.stringify({serial}),
		});
		this.setModal(true, response);
	}

	anonymousView() {
		return (<form onSubmit={this.submitForm}>
			<div class="mb-3">
				<label class="form-label">First Name</label>
				<input type="text" class="form-control" id="firstName" />
			</div>
			<div class="mb-3">
				<label class="form-label">Last Name</label>
				<input type="text" class="form-control" id="lastName" />
			</div>
			<div class="mb-3">
				<label class="form-label">Serial</label>
				<input type="text" class="form-control" id="serial" />
				<div id="serialHelp" class="form-text">The serial provided to enter the draw!</div>
			</div>
			<div class="mb-3">
				<label class="form-label">Email address</label>
				<input type="email" class="form-control" id="email" aria-describedby="emailHelp" />
				<div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>
			</div>
			<div class="mb-3">
				<label class="form-label">Password</label>
				<input type="password" class="form-control" id="password" />
			</div>
			<div class="mb-3 form-check">
				<input type="checkbox" class="form-check-input" id="aboveEighteen" />
				<label class="form-check-label">I Am above 18</label>
			</div>
			<button type="submit" class="btn btn-primary">Submit</button>
		</form>)
	}

	authenticatedView(userName) {
		return (
			<form onSubmit={this.submitFormAuthorized}>
				<h2>Hi {userName}</h2>
				<h3>Here you can submit another serial</h3>
				<div class="mb-3">
					<label class="form-label">Serial</label>
					<input type="text" class="form-control" id="serial" />
					<div id="serialHelp" class="form-text">The serial provided to enter the draw!</div>
				</div>
				<button type="submit" class="btn btn-primary">Submit</button>
			</form>
		);
	}

	toggle = () => this.setModal(!this.state.modal);

	render() {
        const { isAuthenticated, userName } = this.state;
		return (
			<div>
				<div>
					<Modal isOpen={this.state.modal} toggle={this.toggle} className="test">
						<ModalHeader toggle={this.toggle}>Modal title</ModalHeader>
						<ModalBody>
							{this.state.modalText}
						</ModalBody>
						<ModalFooter>
							<Button color="primary" onClick={this.toggle}>Ok</Button>{' '}
						</ModalFooter>
					</Modal>
				</div>
				<h1>Enter the Draw!</h1>
				{isAuthenticated ? this.authenticatedView(userName) : this.anonymousView()}
			</div>
		);
	}
}
