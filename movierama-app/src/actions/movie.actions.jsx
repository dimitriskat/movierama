import { movieConstants } from '../constants';
import { movieService } from '../services';
import { alertActions } from './';

export const movieActions = {
	list,
	sort,
	filterBy,
	removeUserFilter,
	post,
	like,
	hate,
	revokeOpinion
};

function list() {
	return (dispatch, getState) => {
		dispatch(request());

		movieService.list(getState().movies.sorting, getState().movies.filters)
			.then(
				movies => dispatch(success(movies)),
				error => dispatch(failure(error))
			);
	};

	function request() { return { type: movieConstants.LIST_REQUEST } }
	function success(movies) { return { type: movieConstants.LIST_SUCCESS, movies } }
	function failure(error) { return { type: movieConstants.LIST_FAILURE, error } }
}

function sort(field, order) {
	return dispatch => {
		dispatch(updateSorting(field, order));

		dispatch(list())
	};

	function updateSorting(field, order) {
		return {
			type: movieConstants.SORT_UPDATE,
			sorting: {
				field,
				order
			}
		}
	}
}


function filterBy(user, userName) {
	return dispatch => {
		dispatch(updateUserFilter({
			user,
			userName
		}));

		dispatch(list())
	};
}

function removeUserFilter() {
	return dispatch => {
		dispatch(updateUserFilter());

		dispatch(list())
	};
}

function updateUserFilter(filter) {
	return {
		type: movieConstants.USER_FILTER_UPDATE,
		filter
	}
}

function post(movie, redirect) {
	return dispatch => {
		dispatch(request(movie));

		movieService.post(movie)
			.then(
				() => {
					dispatch(success());
					redirect();
					dispatch(alertActions.success('Registration successful'));
				},
				error => {
					dispatch(failure(error));
					dispatch(alertActions.error(error));
				}
			);
	};

	function request(movie) { return { type: movieConstants.POST_REQUEST, movie } }
	function success(movie) { return { type: movieConstants.POST_SUCCESS, movie } }
	function failure(error) { return { type: movieConstants.POST_FAILURE, error } }
}

function like(id) {
	return dispatch => {
		dispatch(request());

		movieService.like(id)
			.then(
				movies => {
					dispatch(success(movies))
					dispatch(list())
				},
				error => dispatch(failure(error))
			);
	};

	function request() { return { type: movieConstants.LIKE_REQUEST } }
	function success() { return { type: movieConstants.LIKE_SUCCESS } }
	function failure(error) { return { type: movieConstants.LIKE_FAILURE, error } }
}

function hate(id) {
	return dispatch => {
		dispatch(request());

		movieService.hate(id)
			.then(
				() => {
					dispatch(success())
					dispatch(list())
				},
				error => dispatch(failure(error))
			);
	};

	function request() { return { type: movieConstants.HATE_REQUEST } }
	function success() { return { type: movieConstants.HATE_SUCCESS } }
	function failure(error) { return { type: movieConstants.HATE_FAILURE, error } }
}

function revokeOpinion(id) {
	return dispatch => {
		dispatch(request());

		movieService.revokeOpinion(id)
			.then(
				() => {
					dispatch(success())
					dispatch(list())
				},
				error => dispatch(failure(error))
			);
	};

	function request() { return { type: movieConstants.REVOKE_OPINION_REQUEST } }
	function success() { return { type: movieConstants.REVOKE_OPINION_SUCCESS } }
	function failure(error) { return { type: movieConstants.REVOKE_OPINION_FAILURE, error } }
}
