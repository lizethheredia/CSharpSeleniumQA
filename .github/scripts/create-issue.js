const runUrl = `https://github.com/${process.env.REPO_OWNER}/${process.env.REPO_NAME}/actions/runs/${process.env.RUN_ID}`;
const allureUrl = "https://lizethheredia.github.io/CSharpSeleniumQA"\;
const date = new Date().toISOString().split('T')[0];

module.exports = async ({ github, context }) => {
  await github.rest.issues.create({
    owner: context.repo.owner,
    repo: context.repo.repo,
    title: `[TEST FAILURE] CI/CD Pipeline failed - ${date}`,
    body: `## Test Failed in CI/CD\n\n**Run:** [View run](${runUrl})\n**Allure Report:** [View report](${allureUrl})\n\n## What to do\n1. Check the Allure report for AI Failure Analysis\n2. Review the screenshot attached to the failing test\n3. Apply the suggested C# fix from Claude\n\n> This issue was created automatically by GitHub Actions`,
  });
};
EOFcat > .github/scripts/create-issue.js << 'EOF'
const runUrl = `https://github.com/${process.env.REPO_OWNER}/${process.env.REPO_NAME}/actions/runs/${process.env.RUN_ID}`;
const allureUrl = "https://lizethheredia.github.io/CSharpSeleniumQA"\;
const date = new Date().toISOString().split('T')[0];

module.exports = async ({ github, context }) => {
  await github.rest.issues.create({
    owner: context.repo.owner,
    repo: context.repo.repo,
    title: `[TEST FAILURE] CI/CD Pipeline failed - ${date}`,
    body: `## Test Failed in CI/CD\n\n**Run:** [View run](${runUrl})\n**Allure Report:** [View report](${allureUrl})\n\n## What to do\n1. Check the Allure report for AI Failure Analysis\n2. Review the screenshot attached to the failing test\n3. Apply the suggested C# fix from Claude\n\n> This issue was created automatically by GitHub Actions`,
  });
};
