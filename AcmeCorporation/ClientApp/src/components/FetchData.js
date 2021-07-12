import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { submissions: [], loading: true };
  }

  componentDidMount() {
    this.populateSubmissionData();
  }

  static renderForecastsTable(submissions) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Email</th>
            <th>Serial</th>
          </tr>
        </thead>
        <tbody>
          {submissions.map(submission =>
            <tr key={submission.email}>
              <td>{submission.email}</td>
              <td>{submission.serial}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.submissions);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateSubmissionData() {
    const token = await authService.getAccessToken();
    const response = await fetch('api/draw/GetAllSubmissions', {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ submissions: data, loading: false });
  }
}
