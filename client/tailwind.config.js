/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx,vue}",
  ],
  theme: {
    extend: {
      colors: {
        "richBlack": "#001219",
        "midnightGreen": "#005F73",
        "darkCyan": "#0A9396",
        "tiffanyBlue": "#94D2BD",
        "vanilla": "#E9D8A6",
        "gamboge": "#EE9B00",
        "alloy": "#CA6702",
        "rust": "#BB3E03",
        "rufous": "#AE2012",
        "auburn": "#9B2226",
        "virgil": "#EDF6F9",
      },
      screens: {
        'xs': '1px',
      }
    },
  },
  plugins: [],
}
