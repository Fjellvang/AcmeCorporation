import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';
import authService from './api-authorization/AuthorizeService'


export class NavMenu extends Component {
	static displayName = NavMenu.name;

	constructor(props) {
		super(props);

		this.toggleNavbar = this.toggleNavbar.bind(this);
		this.state = {
			collapsed: true,
			isAdmin: false,
		};
	}
	componentDidMount() {
		this._subscription = authService.subscribe(() => this.populateState());
		this.populateState();
	}

	async populateState() {
		
		const token = await authService.getAccessToken();
		const response = await fetch("/api/admin/isAdmin", {
			headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
		});
		let isAdmin = false;
		if (response.ok) {
			const json = await response.json();
			isAdmin = json.isAdmin;
		}
		this.setState({
			isAdmin
		});
	}

	toggleNavbar() {
		this.setState({
			collapsed: !this.state.collapsed
		});
	}

	render() {
		return (
			<header>
				<Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
					<Container>
						<NavbarBrand tag={Link} to="/">AcmeCorporation</NavbarBrand>
						<NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
						<Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
							<ul className="navbar-nav flex-grow">
								<NavItem>
									<NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
								</NavItem>
								<NavItem>
									<NavLink tag={Link} className="text-dark" to="/draw">Draw</NavLink>
								</NavItem>
								{this.state.isAdmin ?
									<NavItem>
										<NavLink tag={Link} className="text-dark" to="/submissions">Submissions</NavLink>
									</NavItem>
									: ''}
								<LoginMenu>
								</LoginMenu>
							</ul>
						</Collapse>
					</Container>
				</Navbar>
			</header>
		);
	}
}
