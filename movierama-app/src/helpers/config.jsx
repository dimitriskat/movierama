var apiUrl;

if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
	apiUrl = 'http://localhost:49717/api'
} else {
	apiUrl = 'https://movieramaapi.azurewebsites.net/api'
}

export const config = {
	apiUrl: apiUrl
};
