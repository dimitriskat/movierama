import { loaderConstants } from '../constants';

export function loader(state = {}, action) {
	switch (action.type) {
		case loaderConstants.SHOW:
			return {
				loading: true
			};
		case loaderConstants.HIDE:
			return {
				loading: false
			};
		default:
			return state
	}
}