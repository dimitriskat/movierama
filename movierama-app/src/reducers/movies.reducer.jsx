import { movieConstants } from '../constants';
import { sortingConstants } from '../constants';
import update from 'immutability-helper';

const initialState = {
	movies: [],
	sorting: {
		field: 'likes',
		order: sortingConstants.ASCENDING
	},
	filters: {

	}
}

export function movies(state = initialState, action) {
	switch (action.type) {
		case movieConstants.LIST_SUCCESS:
			return update(state, {
				items: {
					$set: action.movies
				}
			});
		case movieConstants.LIST_FAILURE:
			return update(state, {
				error: {
					$set: action.error
				}
			});
		case movieConstants.SORT_UPDATE:
			return update(state, {
				sorting: {
					$set: action.sorting
				}
			});
		case movieConstants.USER_FILTER_UPDATE:
			var userFilter = action.filter ? {
				id: action.filter.user,
				name: action.filter.userName
			} : undefined;
			return update(state, {
				filters: {
					user: {
						$set: userFilter
					}
				}
			});
		default:
			return state;
	}
}
