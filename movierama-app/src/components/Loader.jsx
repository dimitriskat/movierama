import React from 'react';
import { LinearProgress } from '@material-ui/core';
import { connect } from 'react-redux';

class Loader extends React.Component {
	render() {
		return this.props.loading ? <LinearProgress color="secondary" /> : '';
	}
}

function mapStateToProps(state) {
	const { loader } = state;
	const { loading } = loader;
	return {
		loading
	};
}

const connectedApplicationBar = connect(mapStateToProps)(Loader);
export { connectedApplicationBar as Loader };
