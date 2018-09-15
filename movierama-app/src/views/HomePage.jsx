import React from 'react';
import { connect } from 'react-redux';

import { movieActions } from '../actions';

import MovieList from '../components/MovieList';
import { Divider } from '@material-ui/core';
import ActionBar from '../components/ActionBar';

class HomePage extends React.Component {

	componentDidMount = () => this.props.dispatch(movieActions.list())

	handleMovieLike = (id) => this.props.dispatch(movieActions.like(id))

	handleMovieHate = (id) => this.props.dispatch(movieActions.hate(id))

	handleMovieOpinionRevoke = (id) => this.props.dispatch(movieActions.revokeOpinion(id))

	handleMovieSorting = (field, order) => this.props.dispatch(movieActions.sort(field, order))

	handleShowUserMovies = (userId, userName) => this.props.dispatch(movieActions.filterBy(userId, userName))

	handleShowUserMovies = (userId, userName) => this.props.dispatch(movieActions.filterBy(userId, userName))

	handleRemoveUserFilter = () => this.props.dispatch(movieActions.removeUserFilter())

	render() {
		return (
			<div>
				<ActionBar {...this.props}
					sorting={this.props.movies.sorting}
					filters={this.props.movies.filters}
					onSortFieldClick={this.handleMovieSorting}
					onRemoveUserFilter={this.handleRemoveUserFilter} />
				<Divider />
				<MovieList
					movies={this.props.movies.items}
					loggedIn={this.props.loggedIn}
					user={this.props.user}
					onMovieLike={this.handleMovieLike}
					onMovieHate={this.handleMovieHate}
					onMovieOpinionRevoke={this.handleMovieOpinionRevoke}
					onShowUserMovies={this.handleShowUserMovies} />
			</div>);
	}
}

function mapStateToProps(state) {
	const { authentication, movies } = state;
	const { user, loggedIn } = authentication;
	return {
		loggedIn,
		movies,
		user
	};
}

const connectedHomePage = connect(mapStateToProps)(HomePage);
export { connectedHomePage as HomePage };