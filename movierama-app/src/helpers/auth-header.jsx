export function authHeader() {
	// return authorization header with jwt token
	let user = JSON.parse(localStorage.getItem('user'));

	if (user && user.token) {
		return {
			'Access-Control-Allow-Origin': '*',
			'Authorization': 'Bearer ' + user.token
		};
	} else {
		return {
			'Access-Control-Allow-Origin': '*'
		};
	}
}