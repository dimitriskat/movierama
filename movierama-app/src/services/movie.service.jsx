import { authHeader, config } from '../helpers';

export const movieService = {
	list,
	getById,
	post,
	update,
	like,
	hate,
	revokeOpinion
};

function list(sorting, filters) {
	const requestOptions = {
		method: 'GET',
		headers: { ...authHeader(), 'Content-Type': 'text/plain' }
	};

	var queryParameters = constructQueryParameters(sorting, filters);

	return fetch(config.apiUrl + '/movies' + queryParameters, requestOptions).then(handleResponse, handleError);
}

function constructQueryParameters(sorting, filters) {
	var queryString = '';
	if (sorting) queryString = '?sortField=' + sorting.field + '&sortOrder=' + sorting.order;
	if (filters.user) {
		queryString = (queryString.length == 0) ? '?' : queryString + '&';
		queryString += ('user=' + filters.user.id);
	}
	return queryString;
}

function getById(id) {
	const requestOptions = {
		method: 'GET',
		headers: authHeader()
	};

	return fetch(config.apiUrl + '/movies/' + id, requestOptions).then(handleResponse, handleError);
}

function like(id) {
	const requestOptions = {
		method: 'PUT',
		headers: { ...authHeader(), 'Content-Type': 'application/json' }
	};

	return fetch(config.apiUrl + '/movies/' + id + '/like', requestOptions).then(handleResponse, handleError);
}

function hate(id) {
	const requestOptions = {
		method: 'PUT',
		headers: { ...authHeader(), 'Content-Type': 'application/json' }
	};

	return fetch(config.apiUrl + '/movies/' + id + '/hate', requestOptions).then(handleResponse, handleError);
}

function revokeOpinion(id) {
	const requestOptions = {
		method: 'PUT',
		headers: { ...authHeader(), 'Content-Type': 'application/json' }
	};

	return fetch(config.apiUrl + '/movies/' + id + '/opinionrevoke', requestOptions).then(handleResponse, handleError);
}

function post(movie) {
	const requestOptions = {
		method: 'POST',
		headers: { ...authHeader(), 'Content-Type': 'application/json' },
		body: JSON.stringify(movie)
	};

	return fetch(config.apiUrl + '/movies', requestOptions).then(handleResponse, handleError);
}

function update(movie) {
	const requestOptions = {
		method: 'PUT',
		headers: { ...authHeader(), 'Content-Type': 'application/json' },
		body: JSON.stringify(movie)
	};

	return fetch(config.apiUrl + '/movies/' + movie.id, requestOptions).then(handleResponse, handleError);
}

function handleResponse(response) {
	return new Promise((resolve, reject) => {
		if (response.ok) {
			// return json if it was returned in the response
			var contentType = response.headers.get("content-type");
			if (contentType && contentType.includes("application/json")) {
				response.json().then(json => resolve(json));
			} else {
				resolve();
			}
		} else {
			// return error message from response body
			response.text().then(text => reject(text));
		}
	});
}

function handleError(error) {
	return Promise.reject(error && error.message);
}
