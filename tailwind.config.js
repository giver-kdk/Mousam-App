/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml'
    ],
    theme: {
        extend: {
            colors: {
                primary_bg: '#DBDCFE',
                primary_text: '#454395',
                primary: '#5155EC',
                glass_bg: '#B5BFFC',
                glass_bg_2: '#CFD3F9',
                glass_border: '#E9EBFB',
            },
        },
    },
    plugins: [],
};