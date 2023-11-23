/** @type {import('next').NextConfig} */
const nextConfig = {
    reactStrictMode: process.env.NEXT_PUBLIC_STRICT_MODE ?? true,
    output: 'export',
    images: {
        unoptimized: true
    }
}

module.exports = nextConfig
