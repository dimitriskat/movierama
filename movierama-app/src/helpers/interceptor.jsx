import fetchIntercept from 'fetch-intercept';
import { loaderActions } from '../actions';
import { alertActions } from '../actions';

export function initFetchIntercept(dispatch) {
	fetchIntercept.register({
		
		request: function (url, config) {
			dispatch(alertActions.clear());
			dispatch(loaderActions.show());
			
			// Modify the url or config here
			return [url, config];
		},

		requestError: function (error) {
			dispatch(loaderActions.hide());
			
			// Called when an error occured during another 'request' interceptor call
			return Promise.reject(error);
		},

		response: function (response) {
			dispatch(loaderActions.hide());

			// Modify the reponse object
			return response;
		},

		responseError: function (error) {
			dispatch(loaderActions.hide());

			// Handle an fetch error
			return Promise.reject(error);
		}
	});
}
