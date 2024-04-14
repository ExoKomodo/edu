export default class Constants {
  static languages: string[] = [
    'html',
    'javascript',
    'plain_text',
    'python',
  ];

  static defaultLanguageContent(language: string): string {
    switch (language) {
      case 'html': {
        return `<p>
  content goes here kiddo
</p>`;
      }
      case 'javascript': {
        return `function summing(nums) {
  return nums.reduce((x, y) => x + y);
}

const nums = [];
for (let i = 0; i < 100; i++) {
  nums.push(Math.pow(i, 2));
}
const result = summing(nums);
console.log(result);`;
      }
      case 'plain_text': {
        return `hello world`;
      }
      case 'python': {
        return `import functools
  
from typing import Iterable

def summing(nums: Iterable[int]) -> int:
  return functools.reduce(lambda x, y: x + y, nums)

if __name__ == '__main__':
  # Sum the first hundred squares
  result = summing(
    x ** 2 for x in range(100)
  )
  print(result)`;
      }
      default: {
        return '';
      }
    }
  }
}
