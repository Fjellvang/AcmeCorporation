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
		const password = target.password.value;
		const email = target.email.value;

		//const token = await authService.getAccessToken();
		const response = await fetch('api/draw/submitDraw', {
			method: 'POST',
			//headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
			headers: { 'Content-Type': ' application/json'},
			body: JSON.stringify({email:email, password:password}),
		});
	}

	render() {
		return (
			<div>
				<h1>Counter</h1>
				<form onSubmit={this.submitForm}>
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
						<input type="checkbox" class="form-check-input" id="checkbox" />
						<label class="form-check-label">Check me out</label>
					</div>
					<button type="submit" class="btn btn-primary">Submit</button>
				</form>
			</div>
		);
	}
}
