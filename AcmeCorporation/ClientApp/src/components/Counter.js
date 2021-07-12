import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class Counter extends Component {
	static displayName = Counter.name;

	constructor(props) {
		super(props);
		this.state = { currentCount: 0 };
		this.incrementCounter = this.submitForm.bind(this);
	}

	async submitForm(e) {
		e.preventDefault();
		const target = e.target;

		const aboveEighteen = target.aboveEighteen.checked;
		if (!aboveEighteen) {
			return;// Add some FE coolness...
		}

		const password = target.password.value;
		const email = target.email.value;
		const firstName = target.firstName.value;
		const lastName = target.lastName.value;
		const serial = target.serial.value;

		//const token = await authService.getAccessToken();
		const response = await fetch('api/draw/submitDraw', {
			method: 'POST',
			//headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
			headers: { 'Content-Type': ' application/json'},
			body: JSON.stringify({email:email, password:password, firstName, lastName, serial, aboveEighteen}),
		});
	}

	render() {
		return (
			<div>
				<h1>Counter</h1>
				<form onSubmit={this.submitForm}>
					<div class="mb-3">
						<label class="form-label">First Name</label>
						<input type="text" class="form-control" id="firstName"  />
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
						<label class="form-check-label">Check me out</label>
					</div>
					<button type="submit" class="btn btn-primary">Submit</button>
				</form>
			</div>
		);
	}
}
